function makeTestLive(testId) {
    fetch(`http://localhost:5109/api/Shuffle/${testId}/live`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => console.log(data))
        .catch(error => console.error('Error:', error));
}
