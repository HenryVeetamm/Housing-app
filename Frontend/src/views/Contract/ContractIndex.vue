<template>
    <h1>Index</h1>

        <div class="mb-2">
        <button @click="changeContracts()" v-if="showActive" class="btn btn-info">Pending contracts</button>
        <button @click="changeContracts()" v-if="!showActive" class="btn btn-info">Active contracts</button>
        </div>
    <table class="table table-striped">
        <thead class="table-dark">
            <tr>
                <th>
                    HousingUnit
                </th>
                <th>
                    Lessee
                </th>

                <th>Action</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="item of showingContracts" :class="item.accepted ? 'alert alert-success' : 'alert alert-warning'">
                <td>
                    {{ item.housingUnit?.location }}
                </td>
                <td>
                    {{ item.lessee?.firstName }} {{ item.lessee?.lastName }}
                </td>
                <td>
                    <RouterLink :to="{ name: 'ContractDetails', params: { contractId: item.id } }"
                        class="btn btn-success" type="button">Manage</RouterLink>
                </td>
            </tr>
        </tbody>
    </table>
    


</template>


<script lang="ts">
import { Options, Vue } from "vue-class-component"
import { ContractService } from "@/services/ContractService";
import type { IContract } from "@/domain/IContract";



export default class ContractIndex extends Vue {

    contractService = new ContractService()

    pendingContracts: IContract[] = []
    activeContracts: IContract[] = [];
    showingContracts: IContract[] = [];

    showActive: boolean = true;


    async mounted(): Promise<void> {
        this.pendingContracts = (await this.contractService.GetPendingContracts());
        this.activeContracts = (await this.contractService.GetActiveContracts());
        this.showingContracts = [...this.activeContracts];
    }
    changeContracts(){
        this.showActive = !this.showActive;
        if(this.showActive){
            this.showingContracts = this.activeContracts; 
        }
        if(!this.showActive){
            this.showingContracts = this.pendingContracts; 
        }
    }

}
</script>

<style scoped>
</style>