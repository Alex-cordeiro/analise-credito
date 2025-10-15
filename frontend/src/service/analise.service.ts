import axiosinstance from "@/api/axios";
import type {
  ICriaAnalise,
  ICriaAnaliseResponse,
} from "@/store/analise/interface";

export async function criarNovaAnaliseService(
  novaAnalise: ICriaAnalise
): Promise<ICriaAnaliseResponse> {

    try {
        const { data } = await axiosinstance.post(`/Analise`, novaAnalise);
        return data;
        
    } catch (error) {
        console
    }
}
