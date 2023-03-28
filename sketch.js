function setup() {
    // Create a canvas and place it inside the "robotCanvas" div
    let canvas = createCanvas(290, 200);
    // createCanvas(w,h);
    canvas.parent('robotCanvas');
  
}

function draw() {
    background(255);

    // Head
    fill(123, 130, 134);
    rect(75, 50, 150, 150);
    //antennae
    fill(229,20,20);
    rect(84, 21, 50, 30 );
    rect(168, 21, 50, 30 );

    //eared
    fill(229,20,20);
    rect(25, 75, 50, 30 );
    rect(224, 75, 50, 30 );


    // Eyes

    fill(255);
    rect(84, 75, 50, 30);
    rect(168, 75, 50, 30);
    
   

    // Pupils
    fill(0);
    ellipse(109, 90, 25, 5);
    ellipse(193, 90, 25, 5);
 

    // Mouth
    noFill();
    stroke(0);
    strokeWeight(4);
    arc(150, 140, 60, 50, 0.1, PI - 0.1);

   
}
