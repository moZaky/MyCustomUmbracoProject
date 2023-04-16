import { Route, HashRouter as Router, Routes } from "react-router-dom";
import "./App.css";
import reactLogo from "./assets/react.svg";
import viteLogo from "./assets/vite.png";
import { About } from "./pages/About";
import { ContactUs } from "./pages/Contact";
import { Home } from "./pages/Home";
import { NotFound } from "./pages/NotFound";

export const App = () => {
  return (
    <>
      <nav>
        <h2 className="title">Vite + React + Umbraco</h2>
        <ul>
          <li>
            <a href="/">Home </a>
          </li>
          <li>
            <a href="#/about">About</a>
          </li>
          <li>
            <a href="#/contact">Contact</a>
          </li>
        </ul>
        {/* <ul>
          <li>
            <Link to="/">Home</Link>
          </li>
          <li>
            <Link to="/about">About </Link>
          </li>
          <li>
            <Link to="/contact">Contact</Link>
          </li>
        </ul> */}
      </nav>
      <div className="App">
        <div>
          <a href="https://vitejs.dev" target="_blank">
            <img src={viteLogo} className="logo" alt="Vite logo" />
          </a>
          <a href="https://reactjs.org" target="_blank">
            <img src={reactLogo} className="logo react" alt="React logo" />
          </a>
          <a href="https://reactjs.org" target="_blank">
            <img
              src="https://user-images.githubusercontent.com/6791648/60256231-6e710c00-98d1-11e9-8120-06eecbdb858e.png"
              className="umbraco logo"
              alt="umbraco logo"
            />
          </a>
        </div>
        <Router>
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/about" element={<About />} />
            <Route path="/contact" element={<ContactUs />} />
            <Route path="*" element={<NotFound />} />
          </Routes>
        </Router>
        <div className="card"></div>
      </div>
    </>
  );
};

export default App;
