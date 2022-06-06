<template>
{{billingId}}
    
</template>


<script lang="ts">
import type { IBillingContractService } from "@/domain/IBillingContractService";
import { InitialBilling, type IBilling } from "@/domain/IBilling";
import { BillingContractServiceService } from "@/services/BillingContractServiceService";
import { BillingService } from "@/services/BillingService";
import { Options, Vue } from "vue-class-component";

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
    }







}
</script>

<style scoped>
</style>