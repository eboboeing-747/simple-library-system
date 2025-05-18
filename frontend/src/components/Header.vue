<script>

</script>

<template>
    <header>
        <div class="library-name-area">
            <img class="image" src="https://img.favpng.com/22/3/15/computer-icons-public-library-icon-design-png-favpng-f6uRzdiZz2pY5w7F5nMML571M.jpg">
            <div>libraryName</div>
        </div>
        <div class="search-bar-area">
            <input class="search-bar">
            <button class="search-icon-area">
                <img class="image" style="width: 40px; height: 40px;" src="https://img.icons8.com/ios_filled/512/FFFFFF/search--v3.png">
            </button>
        </div>
        <div class="user-miniprofile-area">
            <div>UserName</div>
            <img class="pfp" src="https://media1.tenor.com/m/jfJjRLN19CwAAAAd/9impulse-impulse.gif">
        </div>
    </header>
</template>

<style scoped>
header {
    top: 0;
    display: flex;
    flex-direction: row;
    height: 70px;
    position: sticky;
    padding: 10px 0px;
    justify-content: space-between;
    background-color: var(--color-background);
}

.library-name-area {
    display: flex;
    flex-direction: row;
    align-items: center;
}

.search-bar-area {
    width: min(40%, 600px);
    display: flex;
    flex-direction: row;
}

.search-bar {
    width: 100%;
    height: 50px;
    border: 2px solid grey;
    border-radius: 30px 0px 0px 30px;
    background-color: transparent;
    padding-left: 30px;
    font-size: 150%;
    color: white;
}

.search-icon-area {
    min-width: 70px;
    width: 70px;
    display: flex;
    align-items: center;
    justify-content: center;
    border-style: solid;
    border-width: 2px 2px 2px 0px;
    border-color: gray;
    border-radius: 0px 30px 30px 0px;
    background-color: transparent;
}

/*
.search-bar-area {
    width: min(50%, 500px);
    height: 50px;
    border-radius: 30px;
    border: 2px solid grey;
    display: flex;
    justify-content: flex-end;
}
*/

/*
.search-bar-icon-area {
    display: flex;
    align-items: center;
    justify-content: center;
    border-left: 2px solid grey;
    width: 75px;
}
*/

.user-miniprofile-area {
    display: flex;
    flex-direction: row;
    align-items: center;
}

.image {
    display: block;
    max-width: 100%;
    max-height: 100%;
    width: auto;
    height: auto;

    margin: 0px 10px;
}

.pfp {
    width: 50px;
    height: 50px;
    border-radius: 50%;
    margin: 0px 10px;
}
</style>