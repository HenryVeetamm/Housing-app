<template>
{{billingId}}
    <!-- <span v-if="!contract.accepted">
        <h1>Request</h1>
    </span>
    <span v-if="contract.accepted">
        <h1>Request</h1>
    </span>

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
    </div> -->
</template>


<script lang="ts">
import type { IBillingContractService } from "@/components/IBillingContractService";
import { EActionType } from "@/domain/Enum/EActionType";
import { InitialHousing, type IHousing } from "@/domain/Housing";
import { InitialBilling, type IBilling } from "@/domain/IBilling";
import { InitialContract, type IContract } from "@/domain/IContract";
import { BillingContractServiceService } from "@/services/BillingContractServiceService";
import { BillingService } from "@/services/BillingService";
import { ContractService } from "@/services/ContractService";
import { HousingService } from "@/services/HousingService";
import { useIdentityStore } from "@/stores/identityStore";
import { Options, Vue } from "vue-class-component";
import { RouterLink } from "vue-router";

@Options({
    components: {
    },
    props: {
        billingId: String,
    },
    emits: [],
})
export default class BillingDetails extends Vue {

    billingId!: string;

    billing: IBilling = InitialBilling;
    billingContractService: IBillingContractService[] = []
    billingService = new BillingService();
    billingContractServiceService = new BillingContractServiceService();


    async mounted(): Promise<void> {
        this.billing = (await this.billingService.getById(this.billingId)).data!
        this.billingContractService = (await this.billingContractServiceService.GetDetailedBilling(this.billingId));
        console.log(this.billing)
        console.log(this.billingContractService)
    }

    async acceptClicked(): Promise<void>{
        
    }

    async addMonthlyPaymentClicked(): Promise<void>{
       
    }





}
</script>

<style scoped>
</style>