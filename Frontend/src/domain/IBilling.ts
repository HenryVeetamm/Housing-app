import type { IContract } from "./IContract";

export interface IBilling{
    id?: string,
    billingMonth: number,
    billingYear: number,
    payed: boolean,
    totalSum: number,
    contractId: string,
    contract?: IContract
}

export const InitialBilling: IBilling = {
    billingMonth: 0,
    billingYear: 0,
    payed: false,
    totalSum: 0,
    contractId: ""
}