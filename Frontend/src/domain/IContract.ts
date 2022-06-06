import type { IHousing } from "./IHousing";
import type { IAppUser } from "./Identity/IAppUser";

export interface IContract{
    id?: string
    housingUnitId: string
    housingUnit?: IHousing
    monthlyRent: number
    startMonth: number,
    startYear: number,
    lesseeId?: string,
    lessee?: IAppUser,
    accepted?: boolean,
    endMonth?: number,
    endYear?: number
}

export const InitialContract: IContract = {
    housingUnitId: "",
    monthlyRent: 0,
    startMonth: 0,
    startYear: 0
}