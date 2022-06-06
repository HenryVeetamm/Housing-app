import { InitialAppUser, type IAppUser } from "./Identity/IAppUser";

export interface IHousing {

    id?: string;
    squareMeters: number
    roomsCount: number
    description: string
    location: string
    amenities: string
    isAvailable: boolean
    pictureUrl: string
    price: number
    dealType: number,
    ownerId: string
    owner?: IAppUser 
}

export const InitialHousing: IHousing = {
    squareMeters: 0,
    roomsCount: 0,
    description: "",
    location: "",
    amenities: "",
    isAvailable: false,
    pictureUrl: "",
    price: 0,
    dealType: 0,
    ownerId: "",
    owner: InitialAppUser
}