<template>
    <div id="app">
        <router-view></router-view>
    </div>
</template>

<script>
    import { ref } from 'vue'

    export default {
        name: 'LayoutDefault',
        setup() {
            return {
                leftDrawerOpen: ref(false),
                value: ref(true),
                slide: ref(1),
                autoplay: ref(true),
            }
        },
    }
</script>

<style>
    #app {
        font-family: Avenir, Helvetica, Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: center;
        color: #2c3e50;
        margin-top: 60px;
    }
</style>
