import { useCallback, useEffect, useReducer, useRef } from 'react'

interface State<T> {
    response?: T
    error?: Error
    loading: boolean;
    // runQuery: (params?: Record<string, any>) => void;
}
interface FetchOptions<T> {
    url?: string;
    options?: RequestInit;
    runOnFirstRender?: boolean;
}

type Cache<T> = { [url: string]: T }

// discriminated union type
type Action<T> =
    | { type: 'loading' }
    | { type: 'fetched'; payload: T }
    | { type: 'error'; payload: Error }

const useFetch = <T = unknown>({ url, options, runOnFirstRender = true }: FetchOptions<T>): State<T> => {
    const cache = useRef<Cache<T>>({})

    // Used to prevent state update if the component is unmounted
    const cancelRequest = useRef<boolean>(false)

    const initialState: State<T> = {
        error: undefined,
        response: undefined,
        loading: false,
        // runQuery: () => (params?: Record<string, any> | undefined): void => {

        // }
    }

    // Keep state logic separated
    const fetchReducer = (state: State<T>, action: Action<T>): State<T> => {
        switch (action.type) {
            case 'loading':
                return { ...initialState, loading: true }
            case 'fetched':
                return { ...initialState, response: action.payload, loading: false }
            case 'error':
                return { ...initialState, error: action.payload, loading: false }
            default:
                return state
        }
    }

    const [state, dispatch] = useReducer(fetchReducer, initialState)
    const runQuery = useCallback((params?: Record<string, any>) => {
        if (url) {
            fetchData(url, params)
        }

    }, [url])
    const fetchData = async (url: string, params?: Record<string, any>) => {
        dispatch({ type: 'loading' })

        // If a cache exists for this url, return it
        if (cache.current[url]) {
            dispatch({ type: 'fetched', payload: cache.current[url] })
            return
        }

        try {
            const response = await fetch(url, options)
            if (!response.ok) {
                throw new Error(response.statusText ? response.statusText : response.status.toString())
            }

            const data = (await response.json()) as T
            cache.current[url] = data
            if (cancelRequest.current) return

            dispatch({ type: 'fetched', payload: data })
        } catch (error) {
            if (cancelRequest.current) return

            dispatch({ type: 'error', payload: error as Error })
        }
    }


    useEffect(() => {
        // Do nothing if the url is not given
        if (!url) return

        cancelRequest.current = false


        if (runOnFirstRender) fetchData(url, options)
        // Use the cleanup function for avoiding a possibly...
        // ...state update after the component was unmounted
        return () => {
            cancelRequest.current = true
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [url, runOnFirstRender])

    return { response: state.response, loading: state.loading, error: state.error, /*runQuery: runQuery */ }
}

export default useFetch
