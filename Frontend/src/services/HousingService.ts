
import type { IHousing } from "@/domain/Housing";
import httpClient from "@/http-client";
import type { AxiosError } from "axios";
import { BaseService } from "./BaseService";
import { IdentityService } from "./IdentityService";

export class HousingService extends BaseService<IHousing>{
    
    constructor() {
        super("housing");
        
    }
   
    async getMyHousing(): Promise<IHousing[]> {

        let response;
        try {
            response = await httpClient.get("/housing/myhousings",
                {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                })
            let res = response.data as IHousing[]
            return res;

        }
        catch (e) {
            let response = (e as AxiosError).response!;
            if (response.status == 401 && this.identityStore.jwt) {
                let identityService = new IdentityService();
                let refreshResponse = await identityService.refreshIdentity();
                this.identityStore.$state.jwt = refreshResponse.data!;

                if (!this.identityStore.$state.jwt) return [];


                response = await httpClient.get("/housing/myhousings",
                    {
                        headers: {
                            "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                        }
                    })

                let res = response.data as IHousing[];
                return res;

            }
        }
        return [];
    }

    async getAvailableHousing(): Promise<IHousing[]> {

        let response;
        try {
            response = await httpClient.get("/housing/availablehousings",
                {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                })
            let res = response.data as IHousing[]
            return res;

        }
        catch (e) {
            let response = (e as AxiosError).response!;
            if (response.status == 401 && this.identityStore.jwt) {
                let identityService = new IdentityService();
                let refreshResponse = await identityService.refreshIdentity();
                this.identityStore.$state.jwt = refreshResponse.data!;

                if (!this.identityStore.$state.jwt) return [];


                response = await httpClient.get("/housing/availablehousings",
                    {
                        headers: {
                            "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                        }
                    })

                let res = response.data as IHousing[];
                return res;

            }
        }
        return [];
    }

}