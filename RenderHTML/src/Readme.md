# Lambda render HTML

This lambda is intended to render HTML using C# with RazorEngine framework.

# Running the lambda

Two objects must be sent:

1- html
   Must contain all the HTML that will be rendered

2- model
  It must contain the object that will serve as a parameter to render the html. Object must be sent as JSON

## Example:

### html
``<!DOCTYPE html><html><body> <div class='divCenter'> <div class='divleft'> <span> Hello, @Model.client </span> <br><br><img src =@Model.image width='150' height='150'> <br><br><span> @Model.about </span> </div></div></body></html>``

### model
``{\"client\": \"{client}\", \"image\" : \"https://images8.alphacoders.com/669/thumbbig-669047.webp\", \"about\" : \" "render html\" }``

### So your body request should be like:

``{
    "html": "<!DOCTYPE html><html><body> <div class='divCenter'> <div class='divleft'> <span> Hello, @Model.client </span> <br><br ><img src=@Model.image width='150' height='150'> <br><br><span> @Model.about </span> </div></div></body>< /html>",
    "model": "{\"client\": \"name client\", \"image\" : \"https://images8.alphacoders.com/669/thumbbig-669047.webp\", \"about \" : \"render html and pdf\" }"
}``

# About the API return

### It will return two attributes.

1- status_code

2- response

Response will be responsible for returning the string with an error message or with the processed html

# About the API

All RazorEngine features are available in cshtml

* for
* while
* linq (when the list is explicit)
* if
* many other features

# Before

<!DOCTYPE html><html><body> <div class='divCenter'> <div class='divleft'> <span> Hello, @Model.client </span> <br><br><img src=@Model.image width='150' height='150'> <br><br><span> @Model.about </span> </div></div></body></html>

# After

<!DOCTYPE html><html><body> <div class='divCenter'> <div class='divleft'> <span> Hello, {name} </span> <br><br><img src=https://images8.alphacoders.com/669/thumbbig-669047.webp width='150' height='150'> <br><br><span> render html </span> </div></div></body></html>
