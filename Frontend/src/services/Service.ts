import type { IService } from "@/domain/IService";

import { BaseService } from "./BaseService";

export class ServiceService extends BaseService<IService>{
    
    constructor() {
        super("service");

    }


}