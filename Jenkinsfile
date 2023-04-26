pipeline {
    agent any
    triggers {
        pollSCM("* * * * *")
    }
    stages {
        stage("Build") {
            steps {
                bat "docker compose build"
            }
        }
        stage("Test") {
            steps {
                bat "docker compose up testdataformatter"
            }
        }
        stage("Deploy") {
            steps {
                try {
                        bat "docker compose up -d"
                    } catch (err) {
                        echo "Deployment failed. Rolling back to previous version..."
                        bat "docker-compose down"
                        bat "docker-compose pull"
                        bat "docker-compose up -d"
            }
        }
        }
        stage("Deliver") {
            steps {
                withCredentials([usernamePassword(credentialsId: 'DockerHub', usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD')]) {
                    bat 'docker login -u %USERNAME% -p %PASSWORD%'
                    bat "docker compose push"
                }
            }
        }
    }
}
