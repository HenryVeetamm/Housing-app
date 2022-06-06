<template>
    <h1>Create</h1>

    <h4>Housing</h4>
    <hr />
    <div class="row">
        <div class="col-md-4">


            <div class="form-group">
                <label class="control-label" for="SquareMeters">SquareMeters</label>
                <input class="form-control" type="number" id="SquareMeters" name="SquareMeters" v-model="housing.squareMeters" />
                
            </div>
            <div class="form-group">
                <label class="control-label" for="RoomsCount">RoomsCount</label>
                <input class="form-control" type="number" id="RoomsCount" name="RoomsCount" v-model="housing.roomsCount" />
                
            </div>
            <div class="form-group">
                <label class="control-label" for="Description">Description</label>
                <textarea class="form-control" id="Description" name="Description" v-model="housing.description">
                </textarea>
               
            </div>
            <div class="form-group">
                <label class="control-label" for="Location">Location</label>
                <input class="form-control" type="text" id="Location" name="Location" v-model="housing.location" />
              
            </div>
            <div class="form-group">
                <label class="control-label" for="Amenities">Amenities</label>
                <textarea class="form-control" id="Amenities" name="Amenities" v-model="housing.amenities">
                </textarea>
            </div>
            <div class="form-group form-check">
                <label class="form-check-label">
                    <input class="form-check-input" type="checkbox" v-model="housing.isAvailable" id="IsAvailable" name="IsAvailable" value="true" />
                    List immediately
                </label>
            </div>
            <div class="form-group">
                <label class="control-label" for="PictureUrl">Add picture url</label>
                <input class="form-control" type="text" id="PictureUrl" name="PictureUrl" v-model="housing.pictureUrl" />
                <span class="text-danger field-validation-valid"></span>
            </div>
            <div class="form-group">
                <label class="control-label" for="Price">Price</label>
                <input class="form-control" type="number" id="Price" name="Price" v-model="housing.price" />
            </div>
            <div class="form-group">
                <label class="control-label" for="DealType">DealType</label>
                <select class="form-control" id="DealType" name="DealType" v-model="housing.dealType">
                    <option v-for="(item, value) in listingType" :value="value">{{ item }}</option>
                </select>
            </div>
            <div class="form-group">
                <input @click="submitClicked()" type="submit" value="Create" class="btn btn-primary" />
            </div>
        </div>
    </div>

    <div>
        <a href="/Admin/Housing">Back to List</a>
    </div>
</template>


<script lang="ts">
import { EActionType } from "@/domain/Enum/EActionType";
import { InitialHousing, type IHousing } from "@/domain/Housing";
import { HousingService } from "@/services/HousingService";
import { useIdentityStore } from "@/stores/identityStore";
import { Options, Vue } from "vue-class-component";
import { RouterLink } from "vue-router";

@Options({
    components: {
    },
    props: {},
    emits: [],
})
export default class HousingCreate extends Vue {


    errorMsg: string[] = [];

    housing: IHousing = InitialHousing;
    housingService = new HousingService();
    identityStore = useIdentityStore();
    listingType: EActionType[] = [EActionType.Sale, EActionType.Rent]



    async mounted(): Promise<void> {
        this.resetHousing();
    }



    checkHousingInfo(): void {
        this.errorMsg = [];

    }

    async submitClicked(): Promise<void> {
        if(this.errorMsg.length == 0){
            let response = await this.housingService.add(this.housing);
            this.$router.push("/housing")
        }
        console.log(this.housing);
    }
    resetHousing() {
        this.housing.squareMeters = 0
    this.housing.roomsCount= 0
    this.housing.description= ""
    this.housing.location= ""
    this.housing.amenities= ""
    this.housing.isAvailable= false
    this.housing.pictureUrl= ""
    this.housing.price= 0
    this.housing.dealType= 0
    
    }

}
</script>

<style scoped>
</style>