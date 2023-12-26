const startButton = document.getElementById('quiz-start-btn');
const nextButton = document.getElementById('quiz-next-btn');
const questionContainerElement = document.getElementById('quiz-question-container');
const questionElement = document.getElementById('quiz-question');
const answerButtonsElement = document.getElementById('quiz-answer-buttons');
const generalText = document.getElementById('quiz-general-text');

let shuffledQuestions, currentQuestionIndex;
let userScore = 0; // Kullanıcının toplam puanını takip etmek için
let answeredQuestion = false; // Kullanıcının bir soruya cevap verip vermediğini kontrol etmek için

startButton.addEventListener('click', startGame);
nextButton.addEventListener('click', () => {
    if (!answeredQuestion) return; // Kullanıcı cevap vermeden sonraki soruya geçemez
    currentQuestionIndex++;
    if (currentQuestionIndex < shuffledQuestions.length) {
        setNextQuestion();
    } else {
        endGame(); // Quiz sona erdiğinde puanı göster
    }
});

function startGame() {
    startButton.classList.add('hide');
    generalText.classList.add('hide')

    shuffledQuestions = questions.sort(() => Math.random() - 0.5);
    currentQuestionIndex = 0;
    userScore = 0; // Yeni oyuna başladığımızda puanı sıfırla
    questionContainerElement.classList.remove('hide');
    setNextQuestion();
}

function setNextQuestion() {
    resetState();
    showQuestion(shuffledQuestions[currentQuestionIndex]);
    answeredQuestion = false; // Yeni soruya geçtiğimizde kullanıcı henüz cevap vermedi
}

function showQuestion(question) {
    questionElement.innerText = question.question;
    const imageContainer = document.querySelector('.quiz-image-container img');
    if (imageContainer) {
        imageContainer.src = question.image;
    }
    question.answers.forEach(answer => {
        const button = document.createElement('quiz-button');
        button.innerText = answer.text;
        button.classList.add('quiz-btn');
        if (answer.correct) {
            button.dataset.correct = answer.correct;
        }
        button.addEventListener('click', () => selectAnswer(answer));
        answerButtonsElement.appendChild(button);
    });
}

function resetState() {
    clearStatusClass(document.body);
    nextButton.classList.add('hide');
    while (answerButtonsElement.firstChild) {
        answerButtonsElement.removeChild(answerButtonsElement.firstChild);
    }
}

function selectAnswer(answer) {
    if (answeredQuestion) return; // Kullanıcı zaten cevap verdi
    answeredQuestion = true;
    const selectedButton = event.target;
    const correct = selectedButton.dataset.correct;

    if (correct) {
        userScore += 5; // Doğru cevap verildiğinde 10 puan ekle
    }

    setStatusClass(document.body, correct);
    Array.from(answerButtonsElement.children).forEach(button => {
        setStatusClass(button, button.dataset.correct);
    });

    nextButton.classList.remove('hide');
}

function setStatusClass(element, correct) {
    clearStatusClass(element);
    if (correct) {
        element.classList.add('correct');
    } else {
        element.classList.add('wrong');
    }
}

function clearStatusClass(element) {
    element.classList.remove('correct');
    element.classList.remove('wrong');
}

function endGame() {
    const imageContainer = document.querySelector('.image-container img');
    if (imageContainer) {
        imageContainer.src = "/img/quizgiris.png";
    }
    const scoreElement = document.createElement('div');
    scoreElement.innerText = `YOU HAVE ${userScore} / 100 POINTS!`;
    scoreElement.classList.add('score');
    document.body.appendChild(scoreElement);

    // Hide question container and buttons
    questionContainerElement.classList.add('hide');
    nextButton.classList.add('hide');
}

const questions = [
    {
        question: 'We have all watched Titanic...So, what do you think is the first scene taped of Titanic?',
        image: "/img/titanic.jpg",
        answers: [
            { text: 'The scene where Jack wins the ticket', correct: false },
            { text: 'The famous Titanic pose where Rose and Jack hugged', correct: false },
            { text: 'The scene where Jack draws Rose', correct: true },
            { text: 'The scene where Jack and Ross dancing', correct: false }
        ]
    },
    {
        question: 'Oscar-winning movie of 2020 The Parasite... Do you remember the order in which our family members entered the villa?',
        image: "/img/parazite.jpeg",
        answers: [
            { text: 'Mom-dad-daughter-son', correct: false },
            { text: 'Son-daughter-dad-mom', correct: true },
            { text: 'Daughter-mom-son-dad', correct: false },
            { text: 'Dad-mom-son-daughter', correct: false }
        ]
    },
    {
        question: 'We\'\ r sure you remember this scene that left its mark on the 2022 Oscar ceremony. So, far which movie Will Smith win the 2022 Oscar?',
        image: "/img/willsmith.jpg",
        answers: [
            { text: 'Emancipation', correct: false },
            { text: 'Kral Richard', correct: true },
            { text: 'Focus', correct: false },
            { text: 'Bad 3', correct: false }
        ]
    },
    {
        question: 'As a result of which movie did our eventful couple Amber Heard and Johnny Deep embark on this love affair?',
        image: "/img/johnnyamber.jpg",
        answers: [
            { text: 'The Rum Diary', correct: true },
            { text: 'Aquaman', correct: false },
            { text: 'Drive Angry', correct: false },
            { text: 'Pirates Of The Carribean 4', correct: false }
        ]
    },
    {
        question: 'The movie Catch Me If You Can managed to impress us with its high tempo. So what was Frank\'\s (Leonardo DiCaprio) crime that warranted his arrest?',
        image: "/img/catch-me-if-you-can.jpeg",
        answers: [
            { text: 'Fake check', correct: true },
            { text: 'Injury', correct: false },
            { text: 'Extortion', correct: false },
            { text: 'Hamesucken', correct: false }
        ]
    },
    {
        question: 'We didn\'\ t forget Jennifer Lawrence, who fell while she was on her way to accept her Oscar, right? We wonder for which film she deserved the award she fell for.',
        image: "/img/jenniferlawrence.jpg",
        answers: [
            { text: 'Don\'\ t look up', correct: false },
            { text: 'Silver Linings Playbook', correct: true },
            { text: 'The Hunger Games', correct: false },
            { text: 'Dark Phoenix', correct: false }
        ]
    },
    {
        question: 'Can you guess the movie series in which 9 people lost their lives, the set burned down and many other events occurred during the filming, and in the end, the director blessed the entire set?',
        image: "/img/horror-movie.jpg",
        answers: [
            { text: 'The Exorcist', correct: true },
            { text: 'Hellraiser', correct: false },
            { text: 'Psycho', correct: false },
            { text: 'Scream', correct: false }
        ]
    },
    {
        question: 'Who do you think is this character who starves himself, sells his house and becomes homeless, and breaks up with his girlfriend in order to better understand the character he plays, and to which movie does it belong?',
        image: "/img/piyanistinfotosu.jpeg",
        answers: [
            { text: 'Cast Away - Tom Hanks', correct: false },
            { text: 'Joker - Joaquin Phoenix', correct: false },
            { text: 'The Platform - Iván Massagué', correct: false },
            { text: 'The Pianist - Adrien Brody', correct: true }
        ]
    },
    {
        question: 'For which movie did Christopher Nolan plant a huge field of corn instead of using visual effects?',
        image: "/img/nolan.jpeg",
        answers: [
            { text: 'The Prestige', correct: false },
            { text: 'Interstellar', correct: true },
            { text: 'Inception', correct: false },
            { text: 'Oppenheimer', correct: false }
        ]
    },
    {
        question: 'Which famous actor almost died after suffering permanent damage as a result of the blows he received in the movie for which he won an Oscar? (Fortunately, he had surgery and came around)',
        image: "/img/georgeclooney.png",
        answers: [
            { text: 'Anthony Hopkins', correct: false },
            { text: 'Heath Ledger', correct: false },
            { text: 'George Clooney', correct: true },
            { text: 'Christian Bale', correct: false }
        ]
    },
    {
        question: 'Who could be the famous actor and what is the his movie who first broke his tooth, then his toes during the shooting, and worst of all, almost drowned by being caught in the current of the river he fell into for the role?',
        image: "/img/viggomortensenjpg.jpg",
        answers: [
            { text: 'Tom Cruise - Mission: Impossible-Fallout', correct: false },
            { text: 'Vin Diesel - Fast & Furious 6', correct: false },
            { text: 'Jason Statham - The Transporter', correct: false },
            { text: 'Viggo Mortensen - The Lord of the Rings: The Two Towers', correct: true }
        ]
    },
    {
        question: 'In the movie Pulp Fiction, Bruce Willis had to return home again because his girlfriend forgot what to take?',
        image: "/img/brucewills.jpeg",
        answers: [
            { text: 'Passport', correct: false },
            { text: 'Heirloom necklace from his mother', correct: false },
            { text: 'Heirloom clock from his father', correct: true },
            { text: 'His lucky boxing glove', correct: false }
        ]
    },
    {
        question: 'Do you remember The Terminal movie that made us laugh out loud? You definitely remember where our main hero was from. (Do you know, this movie is inspired by a real life story)',
        image: "/img/terminal.jpeg",
        answers: [
            { text: 'Ukraine', correct: false },
            { text: 'Russia', correct: false },
            { text: 'Ireland', correct: false },
            { text: 'Krakozia', correct: true }
        ]
    },
    {
        question: 'In the movie The Age of Adaline, which touched us all deeply with its story, William was sure that the Adaline who came to their house was his ex-girlfriend Adaline.',
        image: "/img/theageofadaline.jpg",
        answers: [
            { text: 'Stitch on her finger', correct: true },
            { text: 'Her sleeping position', correct: false },
            { text: 'Her vocabulary', correct: false },
            { text: 'Her favorite food', correct: false }
        ]
    },
    {
        question: 'We are sure that there is no one who hasn\'\ t watched the movie The Curious Case of Benjamin Button. So, which dance was Benjamin\'\ s unforgettable love Betsy, doing?',
        image: "/img/benjaminbutton.jpg",
        answers: [
            { text: 'Ballerina', correct: true },
            { text: 'Salsa', correct: false },
            { text: 'Tango', correct: false },
            { text: 'Vals', correct: false }
        ]
    },
    {
        question: 'In which field is John Nash, played by Russell Crowe in the movie A Beautiful Mind, whose theory is still used today?',
        image: "/img/Beautiful_mind.jpg",
        answers: [
            { text: 'Education', correct: false },
            { text: 'Transportation', correct: false },
            { text: 'Economy', correct: true },
            { text: 'War', correct: false }
        ]
    },
    {
        question: 'Christopher, the main character of the movie The Pursuit of Happiness, What was he trying to make a living for his family by selling?',
        image: "/img/umudunukaybetme.jpeg",
        answers: [
            { text: 'A device related to bones', correct: true },
            { text: 'Some kind of cooler', correct: false },
            { text: 'A functional sound system', correct: false },
            { text: 'Big screen television', correct: false }
        ]
    },
    {
        question: 'Can you guess this movie starring Margot Robbie who added a kissing scene to her movie and her lucky partner? :)',
        image: "/img/margotrobbiebabylon.jpg",
        answers: [
            { text: 'Barbie - Ryan Gosling', correct: false },
            { text: 'Babylon - Brad Pitt', correct: true },
            { text: 'Focus - Will Smith', correct: false },
            { text: 'Amsterdam - Rami Malek', correct: false }
        ]
    },
    {
        question: 'In which movie do you think the crazy director Tim Burton, who took the risk of training the squirrels because he did not want to use visual effects, did this?',
        image: "/img/charliechocolate.jpeg",
        answers: [
            { text: 'Big Fish', correct: false },
            { text: 'Charlie and the Chocolate Factory', correct: true },
            { text: 'Alice in Wonderland', correct: false },
            { text: 'Beetlejuice', correct: false }
        ]
    },
    {
        question: 'A question that seems very simple but actually never comes to mind... How many hours do you think the longest movie in the world is? Don\'\t look at Google :)',
        image: "/img/logistics.jpeg",
        answers: [
            { text: '570', correct: false },
            { text: '1004', correct: false },
            { text: '857', correct: true },
            { text: '365', correct: false }
        ]
    }
]