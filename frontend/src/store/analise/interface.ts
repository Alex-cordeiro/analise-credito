import type ICommonResponse from "@/types/ICommonResponse";

export interface IStore {
  actions: IActions;
  state: IState;
}

export interface IActions {

  criarAnalise: (user: ICriaAnalise) => void;
  retornaStatusAnalise: (cpf: string) => void;
}

export interface IState {
  analise: ICriaAnalise;
  loadings: ILoading;
  analiseResponse: ICriaAnaliseResponse;
  pesquisaAnalise: IPesquisaAnalise;
  pesquisaAnaliseResponse: IPesquisaAnaliseResponse;
}

export interface ICriaAnalise {
  cpf: string;
  nomeCliente: string;
  renda: number;
  logradouro: string;
  email: string;
  telefone: string;
  bairro: string;
  cidade: string;
  estado: string;
  numero: number;
}

export interface ILoading {
  default: boolean;
  create: boolean;
  pesquisa: boolean;
}

export interface IPesquisaAnalise {
  cpf: string;
}

export interface IPesquisaAnaliseResponseContent {
  analiseStatusDescricao: string;
  limiteLiberado: number;
}

export type ICriaAnaliseResponse = ICommonResponse<string[]>
export type IPesquisaAnaliseResponse = ICommonResponse<IPesquisaAnaliseResponseContent>