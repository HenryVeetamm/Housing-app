export interface IAppUser{
    id? : string,
    firstName : string,
    lastName : string,
    nationalCode?: string
}

export const InitialAppUser : IAppUser = {
    id : "",
    firstName: "",
    lastName: ""
}