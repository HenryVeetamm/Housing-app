<template>
    <h1>Contract:</h1>
    <h2>Housing address:{{ housing.location }}</h2>
    <hr />
    <div class="row">
        <div class="input-group col-12 gap-1">
            <div class="form-group">
                <select class="form-control" v-model="extraService">
                    <option :value="null" disabled>Add extra services</option>
                    <option v-for="item in availableServices" :value="item">{{ item.title }} | {{ item.costPerUnit }}$ |
                        {{ item.fixedPricing ? "Fixed price per month" : "Price per unit" }}</option>
                </select>
            </div>
            <div class="form-group">
                <input @click="addService()" type="submit" value="Add service" class="btn btn-primary" />
            </div>
        </div>

    </div>
    <hr />
    <div class="row">
        <div class="col-12">
            Contract services:
            <table class="table table-dark">
                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Service</th>
                        <th scope="col">Remove</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(item, index) in selectedServices">
                        <th scope="row">{{ index }}</th>
                        <th>{{ item.title }} with unit cost of {{ item.costPerUnit }} $</th>
                        <th><input @click="removeService(item)" v-if="item.fixedPricing" type="submit"
                                value="Remove service" class="btn btn-danger" /></th>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="form-group col-3">
            <label class="control-label" for="Title">Month</label>
            <input v-model="startMonth" class="form-control" type="text" id="Title" />
        </div>

    </div>
    <div class="row mb-2">
        <div class="form-group col-3">
            <label class="control-label" for="Title">Year</label>
            <input v-model="startYear" class="form-control" type="text" id="Title" />
        </div>

    </div>

    <div>
        <div class="alert alert-primary">
            Your monthly billing would be: {{ housing.price }}$ + services
        </div>
        <div class="input-group gap-2">
            <div class="form-group">
                <RouterLink to="/housing" class="btn btn-primary">Back to housings</RouterLink>
            </div>
            <div class="form-group">
                <input @click="submitClicked()" value="Make contract" class="btn btn-success" />
            </div>
        </div>

    </div>
</template>


<script lang="ts">
import { EActionType } from "@/domain/Enum/EActionType";
import { InitialHousing, type IHousing } from "@/domain/Housing";
import type { IContract } from "@/domain/IContract";
import type { IContractService } from "@/domain/IContractService";
import type { IService } from "@/domain/IService";
import { ContractService } from "@/services/ContractService";
import { ContractServiceService } from "@/services/ContractServiceService";
import { HousingService } from "@/services/HousingService";
import { ServiceService } from "@/services/Service";
import { useIdentityStore } from "@/stores/identityStore";
import { Options, Vue } from "vue-class-component";
import { RouterLink } from "vue-router";

@Options({
    components: {
    },
    props: { housingId: String, },
    emits: [],
})
export default class ContractCreate extends Vue {

    housingId!: string

    housing: IHousing = InitialHousing;
    housingService = new HousingService();
    serviceService = new ServiceService();
    contractService = new ContractService();
    contractServiceService = new ContractServiceService();

    identityStore = useIdentityStore();

    extraService: IService | null = null;
    startMonth: number | null = null;
    startYear: number | null = null;

    allServices: IService[] = []
    availableServices: IService[] = [];
    selectedServices: IService[] = []

    async mounted(): Promise<void> {
        this.housing = (await this.housingService.getById(this.housingId)).data!;

        this.allServices = (await this.serviceService.getAll())

        this.selectedServices = this.allServices.filter(x => !x.fixedPricing)

        this.availableServices = this.allServices.filter(x => !this.selectedServices.includes(x))
        console.log(this.availableServices)
        

    }

    addService() {
        if (this.extraService != null) {
            this.selectedServices.push(this.extraService!)
            this.availableServices = this.availableServices.filter(x => x.id != this.extraService?.id)
            this.extraService = null
        }
    }

    removeService(item: IService) {
        this.selectedServices = this.selectedServices.filter(x => x.id != item.id)
        this.availableServices.push(item);
    }



    async submitClicked(): Promise<void> {

        let data: IContract = {
            monthlyRent: this.housing.price,
            housingUnitId: this.housingId!,
            startMonth: this.startMonth!,
            startYear: this.startYear!
        }

        let response = await this.contractService.add(data)

        for (const selected of this.selectedServices) {
            let serviceData: IContractService = { contractId: response.data?.id!, serviceId: selected.id! }
            let serviceContractRes = await this.contractServiceService.add(serviceData);
            console.log(serviceContractRes)
        }

        this.$router.push("/contract")

    }


}
</script>

<style scoped>
</style>