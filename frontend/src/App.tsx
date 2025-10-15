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
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
          <div className="flex justify-center gap-4 mb-8"></div>
        </div>
        <Routes>
          <Route Component={CriarAnalise} path="/" />
          <Route Component={ConsultarAnalise} path="/consultar" />
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;
