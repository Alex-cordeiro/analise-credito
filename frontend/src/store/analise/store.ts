import { create } from "zustand";
import { produce } from "immer";
import { initialState } from "./initialState";
import { IStore, IUser, IUserField } from "./interface";
import ICommonResponse from "@/types/common/ICommonResponse<T>";
import {
  createUserService,
  disableUserService,
  updateUserService,
  userListService,
} from "@/service/user/user.service";
import { IPagedOptions } from "@/types/common/IPagedOptions";
import { getTypeFieldsService } from "@/service/introspection/introspection.service";
import { getFieldTypeName } from "@/utils/objectTypeHelper";
import { getUserRoles } from "@/service/utils/utils.service";

export const useUserStore = create<IStore>((set, get) => ({
  state: {
    ...initialState,
  },
  actions: {
    getUsers: async (pagedOptions: IPagedOptions) => {
      set(
        produce(({ state }: IStore) => {
          state.loadings.default = true;
        })
      );

      const data = await userListService(pagedOptions);

      set(
        produce(({ state }: IStore) => {
          if (data.data) {
            state.userList = data;
          } else {
            state.userList = initialState.userList;
          }
        })
      );

      set(
        produce(({ state }: IStore) => {
          state.loadings.default = false;
        })
      );
    },
    updateUser: async (user: IUser) => {
      set(
        produce(({ state }: IStore) => {
          state.loadings.default = true;
        })
      );

      const data = await updateUserService(user);

      set(
        produce(({ state }: IStore) => {
          if (data) {
            state.resultOperation = data;
          }
        })
      );

      set(
        produce(({ state }: IStore) => {
          state.loadings.default = false;
        })
      );
    },
    getUserFields: async () => {
      set(
        produce(({ state }: IStore) => {
          state.loadings.default = true;
        })
      );

      const {
        __type: { fields },
      } = await getTypeFieldsService("User");

      set(
        produce(({ state }: IStore) => {
          if (fields) {
            state.userFields = fields.map(
              (field): IUserField => ({
                name: field.name,
                showName: field.description,
                type: getFieldTypeName(field.type) ?? "",
              })
            );
          } else {
            state.userFields = [];
          }
        })
      );

      set(
        produce(({ state }: IStore) => {
          state.loadings.fields = false;
        })
      );
    },
    disableUser: async (id: string) => {
      set(
        produce(({ state }: IStore) => {
          state.loadings.default = true;
        })
      );

      const data = await disableUserService(id);

      set(
        produce(({ state }: IStore) => {
          if (data) {
            state.resultOperation = data;
          }
        })
      );

      set(
        produce(({ state }: IStore) => {
          state.loadings.default = false;
        })
      );
    },
    createUser: async (user: IUser) => {
      set(
        produce(({ state }: IStore) => {
          state.loadings.default = true;
        })
      );

      const data = await createUserService(user);

      set(
        produce(({ state }: IStore) => {
          if (data) {
            state.resultOperation = data;
          }
        })
      );

      set(
        produce(({ state }: IStore) => {
          state.loadings.default = false;
        })
      );
    },
    setEditUser: (user: IUser) => {
      set(
        produce(({ state }: IStore) => {
          state.user = user;
        })
      );
    },
    getMenusByUser: function (userId: string): void {
      //const data = menusByUserService
    },
    getUserRole: async () => {
      const data = await getUserRoles();
      set(
        produce(({ state }: IStore) => {
          state.userAllAvailableRoles = data;
        })
      );
    },
    sendUserRoles: async () => {},
    setRolesToAssign: async (role: number) => {
      set(
        produce(({ state }: IStore) => {
          const exists = state.user.userRoles.includes(role);
          if (exists) {
            state.user.userRoles = state.user.userRoles.filter(
              (x) => x !== role
            );
          } else {
            state.user.userRoles.push(role);
          }
        })
      );
    },
  },
}));
