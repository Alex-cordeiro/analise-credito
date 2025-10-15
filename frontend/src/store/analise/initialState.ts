import type { IState } from "./interface";


export const initialState: IState = {
  analise: {
    bairro: "",
    cidade: "",
    cpf: "",
    email: "",
    estado: "",
    logradouro: "",
    nomeCliente: "",
    numero: 0,
    renda: 0,
    telefone: "",
  },
  loadings: {
    create: false,
    pesquisa: false,
    default: false,
  },
  analiseResponse: {
    errors: [],
    message: "",
    success: false,
  },
  pesquisaAnalise: {
    cpf: "",
  },
  pesquisaAnaliseResponse: {
    errors: [],
    message: "",
    success: false,
  },
};
