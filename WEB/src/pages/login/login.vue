<script setup>
import { inject, onMounted, ref } from "vue";
// import _apiLoginAZure from "../../api/azureAD-auth";
import _apiAuth from "../../api/auth";
import { setTokenStorage } from "../../helpers/storage";
import modalalert from "../../components/modal-alert.vue";
import { useStore } from "vuex";
const $store = useStore();
const emit = defineEmits(["result-login"]);
const config = ref({});
const formModalAlert = ref({
  label: "",
});
// const emit = defineEmits(['callback'])
const Login = ref({
  Username: "test01@test.com",
  Password: "asdfzxcv",
});
const onAlert = (label) => {
  formModalAlert.value.label = label;
  document.getElementById("modal-alert").showModal();
};
const onLoginSystem = async () => {
  // const params = {
  //   clientId: companySelec.value.ComData.clientToken,
  //   authority: `https://login.microsoftonline.com/${companySelec.value.ComData.tenantToken}`,
  //   redirectUri: config.value.AZURE_REDIRECURI,
  // };
  // const responseAccount = await _apiLoginAZure.login(params);
  // if (responseAccount) {
  
  await _apiAuth.onLogin(
    {
      // Username: "Kantathus",
      // Password: "FS54321#!"

      Username: Login.value.Username,
      Password: Login.value.Password

    },
    (response) => {
      if (response?.Status === true) {
        const setData = {
          token: response.Token,
          sestionInfo: response.body,
        };
        setTokenStorage(setData);
        // console.log("setTokenStorage",response);
        console.log("setTokenStorage", setData);
        emit("result-login", true);
      } else {
        onAlert("Please Contact Administrator");
      }
    }
  );
  // }
};
onMounted(() => {
  $store.commit('setStatusLoading', false)
  config.value = inject("config");
});
</script>
<template>
  <div class="">
    <div class="banner flex flex-row">
      <div class="basis-full  flex justify-center items-center h-screen">
        <div class="bg-white shadow-2xl rounded-lg px-10 py-10 w-2/6 max-w-96">
          <div class="flex flex-col">
            <div class="flex  justify-center pb-2"> <img class="w-100" src="../../assets/images/3b796649-ada2-4058-bbb6-8b55a3c9a1d5.png"></div>
            <span class="mt-2 text-2xl font-extrabold flex justify-center">เข้าสู่ระบบใช้งาน</span>
            <form @submit.prevent="onLoginSystem">
              <div class="mt-6 flex flex-col">
                <label class="text-lg font-bold">ชื่อผู้ใช้</label>
                <input type="text" class="mt-2  h-10 outline outline-2 outline-gray-200 rounded-lg px-3"
                  v-model="Login.Username" placeholder="ชื่อผู้ใช้" />
                <br />
                <label class="text-lg font-bold">รหัสผ่าน</label>
                <input type="password" class="mt-2  h-10 outline outline-2 outline-gray-200 rounded-lg px-3"
                  v-model="Login.Password" placeholder="รหัสผ่าน" />
              </div>
              <button class="mt-9 text-white bg-custom-blue rounded-full w-full h-12 text-lg font-bold">
                เข้าสู่ระบบ
              </button>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
  <!-- <div class="h-screen bg-white overflow-hidden flex flex-col items-center justify-center">
        <img class="" src="../../assets/images/logo2.png">
        <span class="text-3xl font-extrabold">Product Stock V1</span>
        <form @submit.prevent="onLoginSystem">
            <select class="mt-3 outline h-10 px-3 rounded-lg outline-lime-500  w-64" required v-model="companySelection">
                <option value="">-----select-----</option>
                <template v-for="item in config.companyLoginList" :key="item.name">
                    <option :value="item">{{ item.name }}</option>
                </template>
</select><br>
<button class="mt-3 btn btn-primary  w-64"><span class="text-lg text-white">SignIn with
    Email</span></button>
</form>
</div> -->
  <modalalert :formModalAlert=formModalAlert />
</template>
<style scoped>
.banner {
  /* background-image: url("../../../public/images/bannerfull.jpg"); */
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  background: whitesmoke;
}
</style>
