import "./App.css";
import { BrowserRouter, Route } from "react-router";
import CriarAnalise from "./pages/criarAnalise";
import ConsultarAnalise from "./pages/consultarAnalise";

function App() {
  return (
    <>
      <BrowserRouter>
        <Route Component={CriarAnalise} path="/" />
        <Route Component={ConsultarAnalise} path="/consultar" />
      </BrowserRouter>
    </>
  );
}

export default App;
