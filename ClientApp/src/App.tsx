import reactLogo from "./assets/react.svg";
import viteLogo from "./assets/vite.png";
import { HashRouter as Router, Routes, Route, Link } from "react-router-dom";
import { Home } from "./pages/Home";
import { About } from "./pages/About";
import { ContactUs } from "./pages/Contact";
import { NotFound } from "./pages/NotFound";
import "./App.css";

export const App = () => {
  return (
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
      <h1>Vite + React + Umbraco</h1>
      <Router>
        <nav>
          <ul>
            <li>
              <Link to="/">Home</Link>
            </li>
            <li>
              <Link to="/about">About </Link>
            </li>
            <li>
              <Link to="/contact">Contact</Link>
            </li>
          </ul>
        </nav>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/about" element={<About />} />
          <Route path="/contact" element={<ContactUs />} />
          <Route path="*" element={<NotFound />} />
        </Routes>
      </Router>
      <div className="card"></div>
    </div>
  );
};

export default App;
