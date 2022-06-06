
import type { IContract } from "@/domain/IContract";
import httpClient from "@/http-client";
import type { AxiosError } from "axios";

import { BaseService } from "./BaseService";
import { IdentityService } from "./IdentityService";


export class ContractService extends BaseService<IContract>{
    
    constructor() {
        super("contract");

    }

    async GetPendingContracts(): Promise<IContract[]> {

        let response;
        try {
            response = await httpClient.get("/contract/pendingcontracts",
                {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                })
            let res = response.data as IContract[]
            return res;
        }

        catch (e) {
            let response = (e as AxiosError).response!;
            if (response.status == 401 && this.identityStore.jwt) {
                let identityService = new IdentityService();
                let refreshResponse = await identityService.refreshIdentity();
                this.identityStore.$state.jwt = refreshResponse.data!;

                if (!this.identityStore.$state.jwt) return [];


                response = await httpClient.get("/contract/pendingcontracts",
                    {
                        headers: {
                            "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                        }
                    })

                let res = response.data as IContract[];
                return res;
            }
        }
        return [];
    }

    async GetActiveContracts(): Promise<IContract[]> {

        let response;
        try {
            response = await httpClient.get("/contract/activecontracts",
                {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                })
            let res = response.data as IContract[]
            return res;
        }

        catch (e) {
            let response = (e as AxiosError).response!;
            if (response.status == 401 && this.identityStore.jwt) {
                let identityService = new IdentityService();
                let refreshResponse = await identityService.refreshIdentity();
                this.identityStore.$state.jwt = refreshResponse.data!;

                if (!this.identityStore.$state.jwt) return [];


                response = await httpClient.get("/contract/activecontracts",
                    {
                        headers: {
                            "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                        }
                    })

                let res = response.data as IContract[];
                return res;
            }
        }
        return [];
    }



}