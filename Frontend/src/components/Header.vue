<template>
    <header class="animated-background">
        <nav
            class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3 animated-background">
            <div class=" container container-fluid">
                <RouterLink class="navbar-brand" to="/">Home</RouterLink>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse"
                    aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                    <ul class="navbar-nav flex-grow-1">
                       
                        <li>
                            <RouterLink to="/housing" class="nav-link text-dark" active-class="active">
                                All housings</RouterLink>   
                        </li>
                        <template v-if="identityStore.$state.jwt">
                        <li>
                            <RouterLink to="/contract" class="nav-link text-dark" active-class="active">
                                My Contracts</RouterLink>
                        </li>
                         <li>
                            <RouterLink to="/housing/myhousings" class="nav-link text-dark" active-class="active">
                                My housings</RouterLink>
                        </li>
                        <li>
                            <RouterLink to="/billing" class="nav-link text-dark" active-class="active">
                                My billings</RouterLink>
                        </li>
                        </template>
                    </ul>

                    <ul class="navbar-nav">
                        <template v-if="identityStore.$state.jwt == null">
                            <li class="nav-item">
                                <RouterLink class="nav-link text-dark" to="/identity/account/register">Register
                                </RouterLink>
                            </li>
                            <li class="nav-item">
                                <RouterLink to="/identity/account/login" class="nav-link text-dark"
                                    active-class="active">
                                    Login</RouterLink>
                            </li>
                        </template>
                        <template v-else>
                            <li class="nav-item">
                                <div class="nav-link text-dark">Hello {{ identityStore.$state.name }} </div>
                            </li>
                            <li class="nav-item">
                                <a @click="showMoney()" class="nav-link text-dark">Money?</a>
                            </li>
                            <li class="nav-item">
                                <a @click="addMoney()" class="nav-link text-dark">Get money</a>
                            </li>
                        
                            <li class="nav-item">
                                <a @click="logOut()" class="nav-link text-dark">Logout</a>
                            </li>

                        </template>
                    </ul>

                </div>
            </div>
        </nav>
    </header>
</template>


<script lang="ts">
import { Options, Vue } from "vue-class-component";
import { useIdentityStore } from "@/stores/identityStore";
import { RouterLink } from "vue-router";
import { IdentityService } from "@/services/IdentityService";

@Options({
    components: {
    }
})
export default class Header extends Vue {

    identityStore = useIdentityStore();
    identityService = new IdentityService();

    async mounted(): Promise<void> {
        
    }

    async showMoney(): Promise<void>{
        let money =  (await this.identityService.GetUserMoney()).data!;
        alert(`You have ${money}$`)
    }

    async addMoney(): Promise<void>{
        await this.identityService.AddUserMoney()
    }


    logOut(): void {
        this.identityStore.$reset();
        this.$router.push("/")
    }
}
</script>
<style scoped>
</style>



