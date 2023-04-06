import { useEffect } from "react";
import useFetch from "../hooks/use-fetch";
import { HomePage } from "../models/home-page";

export const Home = () => {
  const {
    data: items,
    error,
    //runQuery,
    loading,
  } = useFetch<HomePage>({ url: "/reactcontent/get" });

  useEffect(() => {
    //runQuery();
  }, []);
  return (
    <>
      <h2>Homepage content</h2>
      {loading && <p>loading...</p>}
      {error && <p className="error">error ...{error?.message}</p>}
      {!loading && items && (
        <div>
          {items.title}
          <div>
            {items.imageUrl && (
              <img
                title={items.title}
                src={`${items.imageUrl}`}
                width="auto"
                height="400px"
              />
            )}
          </div>
        </div>
      )}
    </>
  );
};
