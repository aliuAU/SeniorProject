const url = 'https://imdb8.p.rapidapi.com/title/get-reviews?tconst=tt0110475&currentCountry=US&purchaseCountry=US';
const options = {
    method: 'GET',
    headers: {
        'X-RapidAPI-Key': 'de7acf94cdmsh8b56fc076395b9fp14247ajsn195515e5c69b',
        'X-RapidAPI-Host': 'imdb8.p.rapidapi.com'
    }
};
//yarın bakılcak 
async function fetchCriticReviews() {
    try {
        const response = await fetch(url, options);
        const data = await response.json(); // Directly parse the JSON response

        // Check if 'metacritic' and 'reviews' are the correct properties
        if (data && data.metacritic && Array.isArray(data.metacritic.reviews)) {
            // Pass the reviews array to the display function
            displayCriticReviews(data.metacritic.reviews);
        } else {
            console.error('The metacritic reviews property is not an array or does not exist:', data);
        }
    } catch (error) {
        console.error('Error fetching critic reviews:', error);
    }
}

function displayCriticReviews(reviewsArray) {
    const reviewsContainer = document.getElementById('critic-reviews');
    if (!reviewsContainer) {
        console.error('Critic reviews container not found');
        return;
    }

    // Clear previous reviews if any
    reviewsContainer.innerHTML = '';

    // Iterate over the reviews array and create elements for each review
    reviewsArray.forEach(review => {
        const reviewElement = document.createElement('div'); // Use a div to contain each review
        reviewElement.className = 'review';

        const quoteElement = document.createElement('p');
        quoteElement.textContent = review.quote; // Assuming 'quote' is a string
        reviewElement.appendChild(quoteElement);

        const reviewSiteElement = document.createElement('p');
        reviewSiteElement.textContent = `Source: ${review.reviewSite}`; // Assuming 'reviewSite' is a string
        reviewElement.appendChild(reviewSiteElement);

        const reviewerElement = document.createElement('p');
        reviewerElement.textContent = `Reviewer: ${review.reviewer}`; // Assuming 'reviewer' is a string
        reviewElement.appendChild(reviewerElement);

        const scoreElement = document.createElement('p');
        scoreElement.textContent = `Score: ${review.score}`; // Assuming 'score' is a number
        reviewElement.appendChild(scoreElement);

        reviewsContainer.appendChild(reviewElement); // Append each review to the container
    });
}

fetchCriticReviews(); // Call the function