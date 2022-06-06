<template>

    <h1>Index</h1>

    <p>
        <a>
            <RouterLink to="/housing/create" v-if="useIdentity.$state.jwt != null">Add new housing</RouterLink>
        </a>
    </p>
    <div class="col-md-12">
        <div class="input-group gap-1">
            <div class="form-group col-2 mb-2">
                <input class="form-control" v-model="area" v-on:input="filter()" type="text"
                    placeholder="Filter by area" />
            </div>
            <div class="form-group col-2">
                <input class="form-control" v-model="roomCount" v-on:input="filter()" type="text"
                    placeholder="Filter by room count" />
            </div>
            
            <div class="form-group col-2 mb-2">
                <input class="form-control" v-model="location" v-on:input="filter()" type="text"
                    placeholder="Filter by location" />
            </div>
            <div class="form-group col-2 mb-2">
                <input class="form-control" v-model="lowerPrice" v-on:input="filter()" type="text"
                    placeholder="Lowest price" />
            </div>
            <div class="form-group col-2 mb-2">
                <input class="form-control" v-model="upperPrice" v-on:input="filter()" type="text"
                    placeholder="Highest price" />
            </div>
            <div class="form-group">
                <select class="form-control" v-model="dealType" placeholder="Filter by type" v-on:change="filter()">
                    <option :value="null">Choose type</option>
                    <option v-for="(item, index) in listingType" :value="index">{{ item }}</option>
                </select>
            </div>
        </div>
    </div>
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
                <th>
                    Owner
                </th>
                <th></th>
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
                    {{ item.owner!.firstName }} {{ item.owner!.lastName }}
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



export default class HousingIndex extends Vue {

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
        this.housings = await this.HousingService.getAvailableHousing();
        this.searchHousings = [...this.housings]
    }

    filter() {
        this.searchHousings = [...this.housings]
        if (this.roomCount) {
            this.searchHousings = this.searchHousings.filter(x => x.roomsCount >= this.roomCount)
        }
        if (this.area) {
            this.searchHousings = this.searchHousings.filter(x => x.squareMeters >= this.area)
        }
        if (this.location) {
            this.searchHousings = this.searchHousings.filter(x => x.location.toLowerCase().startsWith(this.location.toLowerCase()))
        }
         if (this.lowerPrice) {
            this.searchHousings = this.searchHousings.filter(x => this.lowerPrice <= x.price)
        }
        if (this.upperPrice) {
            this.searchHousings = this.searchHousings.filter(x => this.upperPrice >= x.price)
        }
        if (this.dealType != null) {
            this.searchHousings = this.searchHousings.filter(x => x.dealType == this.dealType)
        }

    }

}
</script>

<style scoped>
</style>