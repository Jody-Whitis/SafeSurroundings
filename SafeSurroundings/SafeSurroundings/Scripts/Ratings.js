$(document).ready(SetStarClass(), SetRating());

function SetStarClass() {
    var links = $(".glyphicon");
    links.click(function () {
        if (this.className == "glyphicon glyphicon-star") {

            var ratingID = this.id; //store the ID that holds the rating name
            $(".glyphicon").each(function () { //loop through all glyps and fill in stars
                if (this.id > ratingID) {
                    this.className = "glyphicon glyphicon-star-empty";
                }
            });

        }

        else {
            this.className = "glyphicon glyphicon-star"; //get the element
            var ratingID = this.id; //store the ID that holds the rating name
            $(".glyphicon").each(function () { //loop through all glyps and fill in stars
                if (this.id < ratingID) {
                    this.className = "glyphicon glyphicon-star";
                }
            });

        }
    })
}

function SetRating() {
    var links = $(".glyphicon");
    links.click(function () {
        console.log(this.id);
        $("#ddlSafetyType").val(this.id);
    });
};
