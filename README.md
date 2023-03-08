# Movie web
## Mock up
Figma: https://www.figma.com/proto/ussq5PcdQAx1fbvIVRK7Jv/Untitled?page-id=0%3A1&node-id=1-5&viewport=-1473%2C1421%2C0.63&scaling=min-zoom
## Main functions
- Sign in
  - Method - POST
  - Params - username **string**, password **string**
  - Return - response to a login request
- Get movies by category id
  - Method - GET
  - Params - categoryId **number**
  - Return - list of movies
- Get movies by genre id, country id and year
  - Method - GET
  - Params - genreId **number**, countryId **number**, year **number**
  - Return - list of movies
- Get movies or actors by title or name(text in search field)
  - Method - GET
  - Return - list of movies and actors
- Get movie by movie id
  - Method - GET
  - Params - staffId **number**
  - Return - movie detailed info
- Get persons by staff id
  - Method - GET
  - Params - staffId **number**
  - Return - person detailed info
- Send review
  - Method - POST
  - Params - userId **number**, movieId **number**, rating **number**, title **string**, title **text**
  - Return - result of the request
## Data models
### User info
Info about user
- Id **number**
- name **string**
- email **string**
- password **string**
### Movie info
Info about movie
- Id **number**
- IdCategory **number**
- title **string**
- tagline **string**
- description **string**
- poster **string**
- year **number**
- world_premiere **date**
- budget **string**
- fees_in_usa **number**
- fees_in_world **number**
- countries 
- actors
- directors
- genres
### Staff info
Info about staff
- Id **number**
- name **string**
- height **number**
- age **number**
- image **string**
- description **string**
- movies
### Reviews info
Info about reviews
- IdUser **number**
- IdMovie **number**
- grade **number**
- title **string**
- text **string**
### Category info
Info about categories
- Id **number**
- name **string**
### Genre info
Info about genres
- Id **number**
- name **string**
- movies
### Country info
Info about countries
- Id **number**
- name **string**
- movies
