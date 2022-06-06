
import type { IBilling } from "@/domain/IBilling";
import httpClient from "@/http-client";
import type { AxiosError } from "axios";

import { BaseService } from "./BaseService";
import { IdentityService } from "./IdentityService";
import type { IServiceResult } from "./IServiceResult";

export class BillingService extends BaseService<IBilling>{
    
    constructor() {
        super("billing");

    }

    async GetUnpaidBillings(): Promise<IBilling[]> {

        let response;
        try {
            response = await httpClient.get("/billing/unpaidbillings",
                {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                })
            let res = response.data as IBilling[]
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

                let res = response.data as IBilling[];
                return res;
            }
        }
        return [];
    }

    async GetPaidBillings(): Promise<IBilling[]> {

        let response;
        try {
            response = await httpClient.get("/billing/paidbillings",
                {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                })
            let res = response.data as IBilling[]
            return res;
        }

        catch (e) {
            let response = (e as AxiosError).response!;
            if (response.status == 401 && this.identityStore.jwt) {
                let identityService = new IdentityService();
                let refreshResponse = await identityService.refreshIdentity();
                this.identityStore.$state.jwt = refreshResponse.data!;

                if (!this.identityStore.$state.jwt) return [];


                response = await httpClient.get("/billing/paidbillings",
                    {
                        headers: {
                            "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                        }
                    })

                let res = response.data as IBilling[];
                return res;
            }
        }
        return [];
    }

    
    async payBilling(id: string): Promise<IServiceResult<void>> {

        let response;
        try {
            
            response = await httpClient.get(`/billing/paybilling/${id}`,
                {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                })
    
        }

        catch (e) {
            let response = (e as AxiosError).response!;
            if (response.status == 401 && this.identityStore.jwt) {
                let identityService = new IdentityService();
                let refreshResponse = await identityService.refreshIdentity();
                this.identityStore.$state.jwt = refreshResponse.data!;

                if (!this.identityStore.$state.jwt) return { status: response.status };


                response = await httpClient.get(`/billing/paybilling/${id}`,
                    {
                        headers: {
                            "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                        }
                    })

                let res = response.data as IBilling[];
                return { status: response.status };
            }
        }
        return { status: 201 };
    }
    

}