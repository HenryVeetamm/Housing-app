<template>
    <h1>Paying bill for housing( {{ contract.housingUnit?.location }})</h1>
    <h2>Paying for: {{ billing.billingMonth }}/{{ billing.billingYear }}</h2>
    <hr />
    <h3>Total: {{ billingSum }}</h3>
    <div v-for="item of contractServicesWithAmount">
        <th>
            <span v-if="!item.added">
                Spent amount: {{ item.service?.title }} | Cost per unit: {{ item.service?.costPerUnit }}
                <input v-model="item.amount" class="form-control" type="number" id="amount" />
                <input @click="addBillingContractService(item)" type="submit" value="Add" class="btn btn-info" />
            </span>
            <span v-if="item.added">
                <div>
                    Cost for {{ item.service?.title! }} is {{ item.amount * item.service?.costPerUnit! }}
                </div>
            </span>
        </th>

    </div>
    <hr />
    <input @click="submitBilling()" type="submit" value="PAY" class="btn btn-success" />
</template>


<script lang="ts">
import type { IBillingContractService } from "@/domain/IBillingContractService";
import { InitialBilling, type IBilling } from "@/domain/IBilling";
import { InitialContract, type IContract } from "@/domain/IContract";
import type { IContractService, IContractServiceHelper } from "@/domain/IContractService";
import { BillingContractServiceService } from "@/services/BillingContractServiceService";
import { BillingService } from "@/services/BillingService";
import { ContractService } from "@/services/ContractService";
import { ContractServiceService } from "@/services/ContractServiceService";
import { useIdentityStore } from "@/stores/identityStore";
import { Options, Vue } from "vue-class-component";

@Options({
    components: {
    },
    props: { billingId: String, },
    emits: [],
})
export default class BillingPay extends Vue {

    billingId!: string

    billingService = new BillingService();
    contractService = new ContractService();
    contractServiceService = new ContractServiceService();
    billingContractServiceService = new BillingContractServiceService();

    billingSum: number = 0

    contract: IContract = InitialContract;
    billing: IBilling = InitialBilling;

    contractServices: IContractService[] = []
    contractServicesWithAmount: IContractServiceHelper[] = []

    identityStore = useIdentityStore();

    async mounted(): Promise<void> {

        this.billing = (await this.billingService.getById(this.billingId)).data!;
        this.contract = (await this.contractService.getById(this.billing.contractId)).data!;

        this.contractServices = (await this.contractServiceService.GetContractServiceByContractId(this.billing.contractId))

        this.billingSum += this.contract.monthlyRent

        this.contractServices.forEach((item: IContractService) => {
            if (item.service?.fixedPricing) {
                this.billingSum += item.service.costPerUnit
                this.contractServicesWithAmount.push({ ...item, amount: 1, added: true });
            } else {
                this.contractServicesWithAmount.push({ ...item, amount: 0, added: false })
            }
        });
    }


    async submitClicked(): Promise<void> {


    }

    async addBillingContractService(conServ: IContractServiceHelper) {
        conServ.added = !conServ.added
        this.billingSum += (conServ.amount * conServ.service?.costPerUnit!)
    }

    async submitBilling(): Promise<void> {
        if (this.checkIfAllAdded()) {
            for (const item of this.contractServicesWithAmount) {
                let data: IBillingContractService = {
                    billingId: this.billingId,
                    contractServiceId: item.id!,
                    amount: item.amount,
                    serviceTotalSum: item.amount * item.service?.costPerUnit!
                }
                let response = await this.billingContractServiceService.add(data)
            }

            let res = await this.billingService.payBilling(this.billingId);
            this.$router.push("/")

        } else {

        }
    }

    checkIfAllAdded(): boolean {

        for (const item of this.contractServicesWithAmount) {
            if (!item.added) {
                return false;
            }
        }
        return true;
    }


}
</script>

<style scoped>
</style>