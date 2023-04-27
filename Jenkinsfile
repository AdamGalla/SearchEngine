pipeline {
    agent any
    triggers {
        pollSCM("* * * * *")
    }
    environment {
        PREV_BUILD_NUMBER = "${env.BUILD_NUMBER.toInteger() - 1}"
    }
    post {
        failure {
            withCredentials([usernamePassword(credentialsId: 'DockerHub', usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD')]) {
                    bat 'docker login -u %USERNAME% -p %PASSWORD%'
                    bat "docker compose down"
                    bat "docker compose pull"
                    bat "set BUILD_NUMBER=%PREV_BUILD_NUMBER%&& docker-compose up --build -d"
                }
           
        }
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
        stage("Deliver") {
            steps {
                withCredentials([usernamePassword(credentialsId: 'DockerHub', usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD')]) {
                    bat 'docker login -u %USERNAME% -p %PASSWORD%'
                    bat "docker compose push"
                }
            }
        }
        stage("Deploy") {
            steps {
                bat "docker compose up -d"
            }
        }
    }
}
