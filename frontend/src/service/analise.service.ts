import axiosinstance from "@/api/axios";
import { initialState } from "@/store/analise/initialState";
import type {
  ICriaAnalise,
  ICriaAnaliseResponse,
  IPesquisaAnalise,
  IPesquisaAnaliseResponse,
} from "@/store/analise/interface";

export async function criarNovaAnaliseService(
  novaAnalise: ICriaAnalise
): Promise<ICriaAnaliseResponse> {
  try {
    const { data } = await axiosinstance.post(`/Analise/Novo`, novaAnalise);
    return data;
  } catch (error) {
    console.log(error);
    return initialState.analiseResponse;
  }
}

export async function consultaAnaliseService(
  pesquisaAnalise: IPesquisaAnalise
): Promise<IPesquisaAnaliseResponse> {
  try {
    const { data } = await axiosinstance.post(
      `/Analise/Consultar`,
      pesquisaAnalise
    );
    return data;
  } catch (error) {
    console.log(error);
    return initialState.pesquisaAnaliseResponse;
  }
}
