<template>
    <div>
        <hr />
        <div>
            <div class="media">
                <div class="media-body">
                    <h2 class="media-heading">Property location: {{ housing.location }}</h2>
                    <h4 class="">Owner {{ housing.owner?.firstName + " " + housing.owner?.lastName }}</h4>

                    <hr />
                    <h3>Description</h3>
                    <p><span style="white-space: pre-line">
                            {{ housing.description }}
                        </span></p>
                    <h3>Amenities</h3>
                    <p><span style="white-space: pre-line">
                            {{ housing.amenities }}
                        </span></p>
                    <h3>Price:</h3>
                    <div>{{housing.price}}</div>
                </div>
            </div>
        </div>
        <div>
            <div class="input-group gap-1">
                <div class="form-group  mb-2">
                    <RouterLink to="/housing" type="button" class="btn btn-info">Back to List</RouterLink>
                </div>
                <div class="form-group col-2 mb-2" v-if="housing.ownerId != identityStore.$state.userId.toString()">
                    <RouterLink :to="{ name: 'ContractCreate', params: { housingId: housing.id } }" v-if="identityStore.$state.jwt" class="btn btn-success"
                        type="button">Sign for housing</RouterLink>
                </div>
            </div>


        </div>
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
    props: {
        id: String,
    },
    emits: [],
})
export default class HousingDetails extends Vue {

    id!: string;

    housing: IHousing = InitialHousing;
    housingService = new HousingService();

    identityStore = useIdentityStore();


    async mounted(): Promise<void> {
        this.housing = (await this.housingService.getById(this.id)).data!;
        console.log(this.housing.location)
    }



}
</script>

<style scoped>
</style>