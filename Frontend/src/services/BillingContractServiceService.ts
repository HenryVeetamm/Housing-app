
import type { IBillingContractService } from "@/domain/IBillingContractService";
import type { IContract } from "@/domain/IContract";
import type { IService } from "@/domain/IService";
import httpClient from "@/http-client";
import type { AxiosError } from "axios";

import { BaseService } from "./BaseService";
import { IdentityService } from "./IdentityService";

export class BillingContractServiceService extends BaseService<IBillingContractService>{
    
    constructor() {
        super("billingcontractservice");

    }


    async GetDetailedBilling(id : string): Promise<IBillingContractService[]> {

        let response;
        try {
            response = await httpClient.get(`/billingcontractservice/detailedbilling/${id}`,
                {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                })
            let res = response.data as IBillingContractService[]
            return res;
        }

        catch (e) {
            let response = (e as AxiosError).response!;
            if (response.status == 401 && this.identityStore.jwt) {
                let identityService = new IdentityService();
                let refreshResponse = await identityService.refreshIdentity();
                this.identityStore.$state.jwt = refreshResponse.data!;

                if (!this.identityStore.$state.jwt) return [];


                response = await httpClient.get(`/billingcontractservice/detailedbilling/${id}`,
                    {
                        headers: {
                            "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                        }
                    })

                let res = response.data as IBillingContractService[];
                return res;
            }
        }
        return [];
    }



}