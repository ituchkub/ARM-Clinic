import Axios from 'axios';
const apiAuthenLogin = `/Login/Authen/UserLogin`
export default {
    config_authen() {
      return {
        'Content-Type': 'application/json',
      }
    },
    async onLogin(parm, sucess) {
        await Axios.post(apiAuthenLogin, parm, this.config_authen()).then((response) => {
          sucess(response.data)
        }).catch(({response}) => {
          sucess(response.data)
        })
    },
}