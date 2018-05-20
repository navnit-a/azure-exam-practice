$params = @{
    Uri = 'https://navzfunapp.azurewebsites.net/api/MyWebHook'
    ContentType = 'application/json'
    Method = 'post'
    body = '{ "first": "Azure", "last": "Functions" }'
}

curl @params