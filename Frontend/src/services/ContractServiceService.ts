import type { IContractService } from "@/domain/IContractService";
import httpClient from "@/http-client";
import type { AxiosError } from "axios";
import { BaseService } from "./BaseService";
import { IdentityService } from "./IdentityService";


export class ContractServiceService extends BaseService<IContractService>{
    
    constructor() {
        super("contractservice");

    }

    async GetContractServiceByContractId(id : string): Promise<IContractService[]> {

        let response;
        try {
            response = await httpClient.get(`/contractservice/contract/${id}`,
                {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                })
            let res = response.data as IContractService[]
            return res;
        }

        catch (e) {
            let response = (e as AxiosError).response!;
            if (response.status == 401 && this.identityStore.jwt) {
                let identityService = new IdentityService();
                let refreshResponse = await identityService.refreshIdentity();
                this.identityStore.$state.jwt = refreshResponse.data!;

                if (!this.identityStore.$state.jwt) return [];


                response = await httpClient.get(`/ContractService/contract/${id}`,
                    {
                        headers: {
                            "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                        }
                    })

                let res = response.data as IContractService[];
                return res;
            }
        }
        return [];
    }
}