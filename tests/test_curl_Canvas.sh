response=$(curl --silent --location --request GET 'https://bth.instructure.com/api/v1/courses/6609
  /assignments/59781/submissions/31494' \
  --header 'Content-Type: application/x-www-form-urlencoded' \
  --header 'Authorization: 12133~FxEx8nVeJkRAzPATmK9VnYTf4MNrV9LwxJZBAkX3cmN9KQheXZR84hHfhnLPVUAX' \
  --fail-with-body)
  
  echo "Raw response: $response"