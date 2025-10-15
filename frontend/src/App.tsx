import "./App.css";
import { BrowserRouter, Route, Routes } from "react-router";
import CriarAnalise from "./pages/criarAnalise";
import ConsultarAnalise from "./pages/consultarAnalise";
import TopBar from "./components/ui/topbar";

function App() {
  return (
    <>
      <BrowserRouter>
        <TopBar />
        <Routes>
          <Route Component={CriarAnalise} path="/" />
          <Route Component={ConsultarAnalise} path="/consultar" />
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;
