const url = 'https://imdb8.p.rapidapi.com/title/get-reviews?tconst=tt2911666&currentCountry=US&purchaseCountry=US';
const options = {
    method: 'GET',
    headers: {
        'X-RapidAPI-Key': 'de7acf94cdmsh8b56fc076395b9fp14247ajsn195515e5c69b',
        'X-RapidAPI-Host': 'imdb8.p.rapidapi.com'
    }
};
async function fetchCriticReviews() {
    try {
        const response = await fetch(url, options);
        const data = await response.json(); 

        if (data && data.metacritic && Array.isArray(data.metacritic.reviews)) {
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

    reviewsContainer.innerHTML = '';

    reviewsArray.forEach(review => {
        const reviewElement = document.createElement('div');
        reviewElement.className = 'review';

        const quoteElement = document.createElement('p');
        quoteElement.textContent = review.quote;
        reviewElement.appendChild(quoteElement);

        const reviewSiteElement = document.createElement('p');
        reviewSiteElement.textContent = `Source: ${review.reviewSite}`;
        reviewElement.appendChild(reviewSiteElement);

        const reviewerElement = document.createElement('p');
        reviewerElement.textContent = `Reviewer: ${review.reviewer}`;
        reviewElement.appendChild(reviewerElement);

        const scoreElement = document.createElement('p');
        scoreElement.textContent = `Score: ${review.score}`;
        reviewElement.appendChild(scoreElement);

        reviewsContainer.appendChild(reviewElement);
    });
}
fetchCriticReviews();