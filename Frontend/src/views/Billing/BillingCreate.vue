<template>

    <h1>Create</h1>

    <h4>Billing</h4>
    <hr />
    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <label class="control-label" for="BillingMonth">Billing month</label>
                <input class="form-control" type="number" id="BillingMonth" name="BillingMonth"
                    v-model="billing.billingMonth" />
            </div>
            <div class="form-group">
                <label class="control-label" for="BillingYear">Billing year</label>
                <input class="form-control" type="number" id="BillingYear" name="BillingYear"
                    v-model="billing.billingYear" />
            </div>
            <div class="form-group">
                <input @click="submitClicked()" type="submit" value="Create" class="btn btn-primary" />
            </div>
        </div>
    </div>

    <div>
        <a href="/Admin/Billing">Back to List</a>
    </div>
</template>


<script lang="ts">
import { InitialBilling, type IBilling } from "@/domain/IBilling";
import { BillingService } from "@/services/BillingService";
import { useIdentityStore } from "@/stores/identityStore";
import { Options, Vue } from "vue-class-component";

@Options({
    components: {
    },
    props: { contractId: String, },
    emits: [],
})
export default class BillingCreate extends Vue {

    contractId!: string

    billingService = new BillingService();
    billing: IBilling = InitialBilling

    identityStore = useIdentityStore();

    async mounted(): Promise<void> {

    }


    async submitClicked(): Promise<void> {
        this.billing.contractId = this.contractId
        let res = await this.billingService.add(this.billing)
        this.$router.push("/contract")
    }


}
</script>

<style scoped>
</style>