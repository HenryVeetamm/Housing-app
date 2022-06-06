import type { EActionType } from "./Enum/EActionType";

export interface IFilterItem {
    roomCount: number,
    area: number,
    location: string,
    price: string,
    dealType: EActionType;
}