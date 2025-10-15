import { create } from "zustand";
import { produce } from "immer";
import { initialState } from "./initialState";
import type { ICriaAnalise, IPesquisaAnalise, IStore } from "./interface";
import {
  consultaAnaliseService,
  criarNovaAnaliseService,
} from "@/service/analise.service";

export const useAnaliseStore = create<IStore>((set, get) => ({
  state: {
    ...initialState,
  },
  actions: {
    criarAnalise: async (analise: ICriaAnalise) => {
      set(
        produce(({ state }: IStore) => {
          state.loadings.default = true;
        })
      );

      const data = await criarNovaAnaliseService(analise);

      set(
        produce(({ state }: IStore) => {
          if (data) {
            state.analiseResponse = data;
          }
        })
      );

      set(
        produce(({ state }: IStore) => {
          state.loadings.default = false;
        })
      );
    },
    retornaStatusAnalise: async (cpf: IPesquisaAnalise) => {
      set(
        produce(({ state }: IStore) => {
          state.loadings.default = true;
        })
      );

      const data = await consultaAnaliseService(cpf);

      set(
        produce(({ state }: IStore) => {
          if (data) {
            state.pesquisaAnaliseResponse = data;
          }
        })
      );

      set(
        produce(({ state }: IStore) => {
          state.loadings.default = false;
        })
      );
    },
    resetResponse: () => {
      set(
        produce(({ state }: IStore) => {
          state.analiseResponse = initialState.analiseResponse;
        })
      );
    },
  },
}));
