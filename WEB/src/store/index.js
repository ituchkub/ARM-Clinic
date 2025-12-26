import { createStore } from 'vuex';
export default createStore({
  state: {
    statusLoading: false,
  },
  mutations: {
    setStatusLoading(state, status){
        state.statusLoading = status
    },
  },
  getters: {
    getStatusLoading: state => state.statusLoading 
  },
  actions: {
    // การดำเนินการที่เกี่ยวข้องกับการเปลี่ยนแปลงสถานะ (state)
  },
  modules: {
    // โมดูล Vuex (ถ้ามี)
  },
});