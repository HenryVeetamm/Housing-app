import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";
import Login from "@/views/Identity/Login.vue"
import Register from "@/views/Identity/Register.vue";
import HousingIndex from "@/views/Housing/HousingIndex.vue";
import HousingCreate from "@/views/Housing/HousingCreate.vue";
import HousingDetails from "@/views/Housing/HousingDetails.vue";
import HousingContractsIndex from "@/views/Housing/HousingContractsIndex.vue";
import ContractCreate from "@/views/Contract/ContractCreate.vue";
import ContractIndex from "@/views/Contract/ContractIndex.vue";
import ContractDetails from "@/views/Contract/ContractDetails.vue";
import MyHousings from "@/views/Housing/MyHousings.vue";
import BillingCreate from "@/views/Billing/BillingCreate.vue";
import BillingIndex from "@/views/Billing/BillingIndex.vue";
import BillingPay from "@/views/Billing/BillingPay.vue";
import BillingDetails from "@/views/Billing/BillingDetails.vue";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {path: "/", name: "home", component: HomeView },
    { path: "/identity/account/login", name: "login", component: Login },
    { path: "/identity/account/register", name: "register", component: Register },
    
    { path: "/housing", name: "Housing", component: HousingIndex },
    { path: "/housing/Create", name: "HousingCreate", component: HousingCreate },
    { path: "/housing/Details", name: "HousingDetails", component: HousingDetails, props: true },
    { path: "/housing/myhousings", name: "MyHousings", component: MyHousings, props: true },

    { path: "/housing/ContractIndex", name: "HousingContractIndex", component: HousingContractsIndex, props: true },

    { path: "/contract", name: "ContractIndex", component: ContractIndex, props: true },
    { path: "/contract/Create", name: "ContractCreate", component: ContractCreate, props: true },
    { path: "/contract/Details", name: "ContractDetails", component: ContractDetails, props: true },

    { path: "/billing", name: "BillingIndex", component: BillingIndex, props: true },
    { path: "/billing/Create", name: "BillingCreate", component: BillingCreate, props: true },
    { path: "/billing/Details", name: "BillingDetails", component: BillingDetails, props: true },
    { path: "/billing/Pay", name: "BillingPay", component: BillingPay, props: true },
    
    
  ],
});

export default router;
