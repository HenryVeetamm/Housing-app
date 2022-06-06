import type { IService } from "./IService";

export interface IContractService{
    id?: string,
    contractId: string,
    serviceId: string,
    service?: IService,
}

export interface IContractServiceHelper{
    id?: string,
    contractId: string,
    serviceId: string,
    service?: IService,
    amount: number
    added: boolean
}