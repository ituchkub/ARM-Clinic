// import home from "./pages/home/home.vue";
import login from "./pages/login/login.vue";
// import worklist from './pages/worklist/worklist.vue'
// import addInsider from './pages/add-insider/add-insider.vue'
// import addProject from './pages/add-project/add-project.vue'
// import manageInsider from './pages/manage-insider/manage-insider.vue'
import settingMaster from './pages/setting-master/setting-master.vue'
// import viewProject from './pages/view-project/view-project.vue'
// import mailLink from './pages/mail-link/mail-link.vue'
// import report from './pages/report/report.vue'
// import manualView from './pages/home/manual-view.vue'
// import mailFound from './pages/mail-link/page-not-found.vue'
const routes = [
  {
    path: "/",
    name: "home",
    component: settingMaster,
    props: true,
  },
  {
    path: "/login",
    name: "login",
    component: login,
    props: true,
  },  
];
export default routes;
