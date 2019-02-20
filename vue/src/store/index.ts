import Vue from 'vue';
import Vuex from 'vuex';
Vue.use(Vuex);
import app from './modules/app'
import session from './modules/session'
import account from './modules/account'
import user from './modules/user'
import role from './modules/role'
import tenant from './modules/tenant'
import pdbrand from './modules/pdbrand'
const store = new Vuex.Store({
    state: {
        //
    },
    mutations: {
        //
    },
    actions: {

    },
    modules: {
        app,
        session,
        account,
        user,
        role,
        tenant,
        pdbrand,
    }
});

export default store;