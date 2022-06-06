<template>


    <h1>Index</h1>

        <div class="mb-2">
        <button @click="changeBilling()" v-if="!showUnpaid" class="btn btn-info">Unpaid billings</button>
        <button @click="changeBilling()" v-if="showUnpaid" class="btn btn-info">Paid billings</button>
        </div>
    <table class="table table-striped">
        <thead class="table-dark">
            <tr>
                <th>
                    HousingUnit
                </th>
                
                <th>Action</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="item of showingBillings" :class="item.payed ? 'alert alert-success' : 'alert alert-danger'">
                <td>

                    {{ item.contract?.housingUnit!.location}} | Date for: {{item.billingMonth}}/{{item.billingYear}}
                </td>
               
                <td>
                    <RouterLink :to="{ name: 'BillingPay', params: { billingId: item.id } }" v-if="!item.payed"
                        class="btn btn-danger" type="button">Pay</RouterLink>
                    <RouterLink :to="{ name: 'BillingDetails', params: { billingId: item.id } }" v-if="item.payed"
                        class="btn btn-danger" type="button">Details</RouterLink>
                </td>
            </tr>
        </tbody>
    </table>



</template>


<script lang="ts">
import { Options, Vue } from "vue-class-component"
import type { IBilling } from "@/domain/IBilling";
import { BillingService } from "@/services/BillingService";
import { RouterLink } from "vue-router";



export default class BillingIndex extends Vue {

    billingService = new BillingService()

    unpaidBillings: IBilling[] = []
    paidBillings: IBilling[] = [];
    showingBillings: IBilling[] = [];

    showUnpaid: boolean = true;


    async mounted(): Promise<void> {
        this.unpaidBillings = (await this.billingService.GetUnpaidBillings());
        this.paidBillings = (await this.billingService.GetPaidBillings());
        this.showingBillings = [...this.unpaidBillings];
    }
    changeBilling(){
        this.showUnpaid = !this.showUnpaid;
        if(this.showUnpaid){
            this.showingBillings = this.unpaidBillings; 
        }
        if(!this.showUnpaid){
            this.showingBillings = this.paidBillings; 
        }
    }

}
</script>

<style scoped>
</style>