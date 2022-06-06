<template>

    <h1>Index</h1>
    <table class="table table-striped table">
        <thead class="table-dark">
            <tr>
                <th>
                    Area
                </th>
                <th>
                    Rooms
                </th>
                <th>
                    Description
                </th>
                <th>
                    Location
                </th>
                <th>
                    Amenities
                </th>
                <th>
                    Is available
                </th>
                <th>
                    Price
                </th>
                <th>
                    Deal type
                </th>
                
                <th>Action</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="item of searchHousings" :key="item.id">
                <td>
                    {{ item.squareMeters }} m3
                </td>
                <td>
                    {{ item.roomsCount }}
                </td>
                <td>
                    {{ item.description }}
                </td>
                <td>
                    {{ item.location }}
                </td>
                <td>
                    {{ item.amenities }}
                </td>
                <td>
                    <input :checked="item.isAvailable" class="check-box" :disabled="true" type="checkbox" />
                </td>
                <td>
                    {{ item.price }}
                </td>
                <td>
                    {{ item.dealType == 0 ? "Sale" : 'Rent'}}
                </td>
                
                <td>
                    <RouterLink :to="{ name: 'HousingDetails', params: { id: item.id } }" class="btn btn-info"
                                    type="button">View housing Details</RouterLink>
                </td>
            </tr>
        </tbody>
    </table>

</template>


<script lang="ts">
import { Options, Vue } from "vue-class-component"
import { useIdentityStore } from "@/stores/identityStore";
import { HousingService } from "@/services/HousingService";
import type { IHousing } from "@/domain/IHousing";
import { EActionType } from "@/domain/Enum/EActionType";



export default class MyHousings extends Vue {

    roomCount!: number;
    area!: number
    location!: string
    lowerPrice!: number
    upperPrice!: number
    dealType!: null;
    listingType: EActionType[] = [EActionType.Sale, EActionType.Rent]

    HousingService = new HousingService();
    housings: IHousing[] = []
    searchHousings: IHousing[] = []
    useIdentity = useIdentityStore();

    async mounted(): Promise<void> {
        this.housings = await this.HousingService.getMyHousing();
        this.searchHousings = [...this.housings]
    }

}
</script>

<style scoped>
</style>