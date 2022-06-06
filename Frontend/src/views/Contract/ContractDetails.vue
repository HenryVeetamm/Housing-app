<template>

    <h1>Request</h1>

    <div>
        <h4>Contract</h4>
        <hr />
        <dl class="row">
            <dt class="col-sm-2">
                Housing location:
            </dt>
            <dd class="col-sm-10">
                {{ contract.housingUnit?.location }}
            </dd>
            <dt class="col-sm-2">
                Lessee
            </dt>
            <dd class="col-sm-10">
                {{ contract.lessee?.firstName }} {{ contract.lessee?.lastName }}
            </dd>
            <dt class="col-sm-2">
                MonthlyRent
            </dt>
            <dd class="col-sm-10">
                {{ contract.monthlyRent }}
            </dd>
            <dt class="col-sm-2">
                Since:
            </dt>
            <dd class="col-sm-10">
                {{ contract.startMonth }}/{{ contract.startYear }}
                <span v-if="contract.endMonth">
                    - {{ contract.endMonth }}/{{ contract.endYear }}
                </span>
            </dd>
            <dt class="col-sm-2">
                Actions:
            </dt>
            <dd class="col-sm-10">
               <div class="form-group">
                   <span v-if="!contract.accepted">
                <input @click="acceptClicked()" type="submit" value="Accept" class="btn btn-primary" />
                </span>
                <span v-if="contract.accepted">
                <RouterLink :to="{ name: 'BillingCreate', params: { contractId: contract.id } }" class="btn btn-info"
                                    type="button">Add billing</RouterLink>
                </span>          
            </div>
            </dd>
            <div>
            </div>
        </dl>
    </div>
</template>


<script lang="ts">
import { InitialContract, type IContract } from "@/domain/IContract";
import { ContractService } from "@/services/ContractService";
import { Options, Vue } from "vue-class-component";
import { RouterLink } from "vue-router";

@Options({
    components: {
    },
    props: {
        contractId: String,
    },
    emits: [],
})
export default class HousingDetails extends Vue {

    contractId!: string;

    contract: IContract = InitialContract;
    contractService = new ContractService();



    async mounted(): Promise<void> {
        this.contract = (await this.contractService.getById(this.contractId)).data!
        console.log(this.contract)
    }

    async acceptClicked(): Promise<void>{
        console.log(this.contract.accepted)
        this.contract.accepted = true
        let response = await this.contractService.update(this.contract, this.contractId)
        this.$router.push("/contract")
    }

    async addMonthlyPaymentClicked(): Promise<void>{
        console.log(this.contract.accepted)
        this.contract.accepted = true
        let response = await this.contractService.update(this.contract, this.contractId)
        this.$router.push("/contract")
    }





}
</script>

<style scoped>
</style>