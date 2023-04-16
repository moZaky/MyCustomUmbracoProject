type Props = { loading: boolean };
const Spinner = ({ loading }: Props) => (
  <div className="center"> {loading && <div className="loader"></div>}</div>
);

export default Spinner;
